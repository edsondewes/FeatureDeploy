(function ($, window) {
    "use strict";

    var FD = window.FD;

    function Project(name) {
        this.name = name;
        this.features = [];
    }

    Project.prototype.load = function (callback) {
        var that = this;
        FD.controller.features(this.name, function (data) {
            $.each(data, function (i, obj) {
                var feature = new Feature(that.name, obj);
                that.features.push(feature);
            });

            callback.apply(that);
        });
    };

    Project.prototype.render = function (container) {
        var that = this,
            html = $(FD.template.project.render(this));

        html.find('.feature').each(function (i, obj) {
            that.features[i].render(obj);
        });

        container.append(html);
    };

    function Feature(project, data) {
        this.project = project;
        this.data = data;
        this.statusClass = data.Status === 1 ? 'success' : 'fail';
    }

    Feature.prototype.deploy = function () {
        var that = this,
            loading = $(FD.template.loader.render()),
            container = this.btnDeploy.parent();

        this.btnDeploy.hide();
        container.prepend(loading);

        FD.controller.deploy(this.project, this.data.Id, function (success) {
            that.btnDeploy.show();
            loading.remove();

            FD.template.message.render(container, success);
        });
    };

    Feature.prototype.render = function (html) {
        this.btnDeploy = $(html).find('.deploy');
        var that = this;

        this.btnDeploy.on('click', function (e) {
            that.deploy();
            e.preventDefault();
        });
    };

    $(function () {
        var body = $('body'),
            pageLoader = $('.page-loader');

        FD.controller.listProjects(function (projects) {
            $.each(projects, function (x, p) {
                var project = new Project(p);
                project.load(function () {
                    pageLoader.remove();
                    this.render(body);
                });
            });
        });
    });
})(jQuery, this);