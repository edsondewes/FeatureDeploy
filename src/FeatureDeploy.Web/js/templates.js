(function ($, Mustache, window) {
    "use strict";

    var FD = window.FD || {};

    function featuresList() {
        var template = $('#featuresList').html();
        Mustache.parse(template);

        return {
            render: function (project) {
                return Mustache.render(template, project);
            }
        };
    }

    function gifLoader() {
        var template = $('#gifLoader').html();
        Mustache.parse(template);

        return {
            render: function () {
                return Mustache.render(template);
            }
        };
    }

    function lineMessage() {
        var template = $('#lineMessage').html();
        Mustache.parse(template);

        return {
            render: function (container, success) {
                var html = $(Mustache.render(template));

                html.find('.close').on('click', function (e) {
                    html.remove();
                    e.preventDefault();
                });

                html.addClass(success ? 'success' : 'fail');
                container.prepend(html);
            }
        };
    }

    FD.template = {
        project: featuresList(),
        loader: gifLoader(),
        message: lineMessage()
    };

    window.FD = FD;
})(jQuery, Mustache, this);