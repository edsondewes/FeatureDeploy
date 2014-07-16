(function ($, window) {
    "use strict";

    var FD = window.FD || {};

    function apiController() {
        var baseUrl = 'api/project/';

        return {
            listProjects: function (callback) {
                $.getJSON(baseUrl + 'list', callback);
            },
            features: function (projectName, callback) {
                $.getJSON(baseUrl + 'features', { projectName: projectName }, callback);
            },
            deploy: function (projectName, buildId, callback) {
                $.ajax({
                    url: baseUrl + 'deploy',
                    data: { projectName: projectName, buildId: buildId }
                }).done(function () {
                    callback(true);
                }).fail(function () {
                    callback(false);
                });
            }
        };
    }

    function fakeController() {
        return {
            listProjects: function (callback) {
                callback([{
                    Name: 'Project 1',
                    Url: 'http://www.site.com'
                }, {
                    Name: 'Project 2',
                    Url: undefined
                }]);
            },
            features: function (projectName, callback) {
                var builds = [{
                    Id: '1',
                    Branch: 'FEATDEP-10',
                    Number: '1.1.15',
                    Status: 1
                }, {
                    Id: '2',
                    Branch: 'FEATDEP-11',
                    Number: '1.2.5',
                    Status: 0
                }];

                callback(builds);
            },
            deploy: function (projectName, buildId, callback) {
                var success = Math.random() > 0.5 ? true : false;
                setTimeout(function () { callback(success); }, 2000);
            }
        };
    }

    FD.controller = apiController();
    window.FD = FD;
})(jQuery, this);