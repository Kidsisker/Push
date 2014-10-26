/*
*
* Utilities.
*
*/
PushUI.util.dialog = function (args) {
    var isLoading = false;
    // set defaults
    args.position = args.position || { my: "center", at: "top", of: window };
    args.modal = args.modal == undefined ? true : false;
    args.resizable = args.resizable == undefined ? false : true;
    args.close = args.close || (!args.hideOnClose ? function () { $(args.container).empty(); } : null);
    this.apply = function (opts) {
        for (var p in opts) {
            args[p] = opts[p];
        }
    };
    this.getContainer = function () {
        return $(args.container);
    };
    this.show = function (msg) {
        var container = this.getContainer();
        isLoading = !msg;
        msg = isLoading ? '<div class="ajax-loading-md"></div>' : msg; //show activity indicator if no msg passed.
        container.html(msg);
        container.dialog(args);
        if (isLoading) {
            PushUI.util.events.ajaxCallback.error.bind(container.parent(), function () {
                $(".ui-dialog-titlebar-close", container.parent()).show();
            });
            $(".ui-dialog-titlebar-close", container.parent()).hide();
        }
        else
            $(".ui-dialog-titlebar-close", container.parent()).show();
        if (!args.height)
            container.dialog(args).height("auto");
    };
    this.close = function () {
        try {
            this.getContainer().dialog("close");
        }
        catch (e) { }
    };
    this.destroy = function () {
        try {
            this.getContainer().dialog("destroy");
        }
        catch (e) { }
    };
    this.unhide = function () {
        try {
            this.getContainer().dialog("open");
        }
        catch (e) { }
    };
};

PushUI.util.alertError = {
    container: "#pushUiErrors",
    show: function (msg) {
        msg = msg || "Error. Please try again.";
        msg = "<p><span class=\"ui-icon ui-icon-alert\"></span>" + msg + "</p>";
        $(this.container).html(msg);
        $(this.container).dialog({
            modal: true,
            resizable: false,
            title: "Error",
            position: { my: "center", at: "top", of: window },
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
};

PushUI.util.alertInfo = {
    container: "#pushUiErrors",
    show: function (msg) {
        msg = "<p><span class=\"ui-icon ui-icon-info\"></span>" + msg + "</p>";
        $(this.container).html(msg);
        $(this.container).dialog({
            modal: true,
            resizable: false,
            title: "Attention",
            position: { my: "center", at: "top", of: window },
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
};

PushUI.util.confirm =
{
    container: "#pushUiErrors",
    show: function (args) {
        args.body = args.body || "";
        args.msg = "<p><span class=\"ui-icon ui-icon-info\"></span>" + args.msg + "</p>" + args.body;
        $(this.container).html(args.msg);
        $(this.container).dialog({
            modal: true,
            resizable: false,
            title: args.title || "Confirm",
            position: { my: "center", at: "top", of: window },
            buttons: args.buttons || {
                Yes: function () {
                    PushUI.util.confirm.events.submit.trigger(this);
                },
                No: function () {
                    PushUI.util.confirm.events.cancel.trigger(this);
                    $(this).dialog("close");
                }
            }
        });
    }
};

PushUI.util.events = {
    ajaxCallback: {
        complete: {
            type: "ajaxCallbackCompleteEvent",
            bind: function (obj, fn) {
                $(obj).bind(this.type, function (e, data) { fn(e, data); });
            },
            trigger: function (data) {
                $.event.trigger(this.type, data);
            }
        },
        success: {
            type: "ajaxCallbackSuccessEvent",
            bind: function (obj, fn) {
                $(obj).bind(this.type, function (e, data) { fn(e, data); });
            },
            trigger: function (data) {
                $.event.trigger(this.type, data);
            }
        },
        error: {
            type: "ajaxCallbackErrorEvent",
            bind: function (obj, fn) {
                $(obj).bind(this.type, function (e, data) { fn(e, data); });
            },
            trigger: function (data) {
                $.event.trigger(this.type, data);
            }
        }
    }
};

PushUI.util.ajaxCallback = function (xhr, error) {
    PushUI.util.events.ajaxCallback.complete.trigger(xhr);
    this.response = xhr;
    this.responseJson = (this.response && typeof this.response === "object") ? this.response : null;
    this.responseJsonError = (this.responseJson && this.responseJson.errormessage && this.responseJson.errormessage.length > 0) ? this.responseJson.errormessage : "";
    this.responseIsRedirect = this.responseJson && this.responseJson.redirect && this.responseJson.redirect.length > 0;
    this.error = error || this.responseIsRedirect || this.responseJsonError.length > 0;
    this.handleRedirect = function () {
        if (this.responseIsRedirect) {
            location.href = this.responseJson.redirect;
        }
    };
    this.handleSuccess = function () {
        if (this.error)
            this.handleError();
        else
            PushUI.util.events.ajaxCallback.success.trigger(xhr);
    };
    this.handleError = function () {
        if (this.responseIsRedirect)
            this.handleRedirect();
        else {
            $(document).bind("loading.facebox", function () {
                $("#facebox .close").unbind("click").click(function () {
                    // some popups will disable close button to show/hide popups "underneath"
                    $(document).trigger("close.facebox");
                });
            });
            if (this.responseJsonError.length > 0)
                PushUI.util.alertError.show(this.responseJsonError);
            else if (this.response.length > 0)
                PushUI.util.alertError.show(this.response);
            PushUI.util.events.ajaxCallback.error.trigger(xhr);
        }
    };
};

PushUI.util.jQueryAjaxCallback = function (event, xhr, options, error) {
    this.response = xhr.responseText || "";
    this.responseJson = (this.response && options.dataType == "json") ? JSON.parse(this.response) : null;
    var handler = new PushUI.util.ajaxCallback(this.responseJson || this.response, error);
    return !error ? handler.handleSuccess() : handler.handleError();
};

$(document).ajaxSuccess(function (event, xhr, options) {
    new PushUI.util.jQueryAjaxCallback(event, xhr, options);
});
$(document).ajaxError(function (event, xhr, options) {
    new PushUI.util.jQueryAjaxCallback(event, xhr, options, true);
});