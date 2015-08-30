(function () {
    'use strict';
    //create angularjs controller
    var app = angular.module('mytop5', ['ngTagsInput']);//set and get the angular module
    app.controller('top5listController', ['$scope', '$http', top5listController]);
    app.controller('searchController', ['$scope', '$http', searchController]);
    app.controller('UploadCtrl', ['$scope', '$http', '$timeout', '$upload', UploadCtrl]);

    //angularjs controller method
    function top5listController($scope, $http) {

        //declare variable for mainain ajax load and entry or edit mode
        $scope.loading = true;
        $scope.addMode = false;

        //$scope.tags = [];

        /*$http.get('www.google.com/someapi', {
            headers: { 'Authorization': 'Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==' }
        });*/

        //get all top5list information
        $http.get('/api/top5list/', {
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
        }).success(function (data) {
            $scope.top5lists = data;
            $scope.loading = false;
        })
        .error(function () {
            $scope.error = "An Error has occured while loading top5lists!";
            $scope.loading = false;
        });

        //Get top5list items
       /* $scope.loadItems = function ($query) {
            //return $http.get('/api/Tags/Tag');
            return ["Tag1", "Tag2", "Tag3", "Tag4", "Tag5"];
            /*$http.get('/api/Tags/'+query).success(
            function (data) {
                $scope.tags = data;
            }).error(function (data) {
                $scope.error = "An Error has occured while getting items! " + data;
                $scope.loading = false;
            });*/
        //};

        /*$scope.loadItems = function ($query) {
            return ['Tag1', 'Tag2', 'Tag3', 'Tag4', 'Tag5'];
        };*/

        //Get top5list items
        $scope.showItems = function () {
            this.top5list.itemsMode = true;
            $scope.loading = true;
            var Id = this.top5list.top5ListId;
            $scope.tags = this.top5list.tags;
            $http.get('/api/top5list/'+Id+'/items/',  {
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
        }).success(
            function (data) {
                $.each($scope.top5lists, function (i) {
                    if ($scope.top5lists[i].top5ListId === Id) {
                        $scope.top5lists[i].top5listitems = data;
                    }
                });
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while getting items! " + data;
                $scope.loading = false;
            });
        };

        //by pressing toggleEdit button ng-click in html, this method will be hit
        $scope.hideItems = function () {
            this.top5list.itemsMode = !this.top5list.itemsMode;
        };

        //by pressing toggleEdit button ng-click in html, this method will be hit
        $scope.toggleEdit = function () {
            this.top5list.editMode = !this.top5list.editMode;
        };

        //by pressing toggleAdd button ng-click in html, this method will be hit
        $scope.toggleAdd = function () {
            $scope.addMode = !$scope.addMode;
        };

        //by pressing toggleAdd button ng-click in html, this method will be hit
        $scope.toggleItemAdd = function () {
            this.top5list.addItemMode = !this.top5list.addItemMode;
        };

        //Inser top5list
        $scope.add = function () {
            $scope.loading = true;
            $http.post('/api/top5list/', this.newtop5list,  {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
            }).success(
            function (data) {
                $scope.addMode = false;
                $scope.top5lists.push(data);
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Adding top5list! " + data;
                $scope.loading = false;
            });
        };

        //Inser top5listItem
        $scope.addItem = function () {
            $scope.loading = true;
            this.newtop5listitem.top5listId = this.top5list.top5ListId;
            var frien = this.top5list;
            $http.post('/api/top5listitem/', this.newtop5listitem, {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
            }).success(
            function (data) {
                frien.addItemMode = false;
                frien.top5listitems.push(data);
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Adding top5list! " + data;
                $scope.loading = false;
            });
        };

        $scope.tagAdded = function (tag) {
            $scope.loading = true;
            tag.top5ListId = this.top5list.top5ListId;
            $http.post('/api/Tags/', tag, {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
            }).success(function (data) {
                tag.tagId = data.tagId;
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Saving tag! " + data;
                $scope.loading = false;
            });
        };

        $scope.tagRemoved = function (tag) {
            if (tag.tagId == 0)
                return;
            $scope.loading = true;
            tag.top5ListId = this.top5list.top5ListId;
            var Id = tag.tagId;
            $http.delete('/api/Tags/' + Id, {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
            }).success(function (data) {
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Delete tag! " + data;
                $scope.loading = false;
            });
        };


        //Edit top5list
        $scope.save = function () {
            $scope.loading = true;
            var frien = this.top5list;
            $http.put('/api/top5list/' + frien.top5ListId, frien, {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
            }).success(function (data) {
                frien.editMode = false;
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Saving top5list! " + data;
                $scope.loading = false;
            });
        };

        //Delete top5list
        $scope.deletetop5list = function () {
            $scope.loading = true;
            var Id = this.top5list.top5ListId;
            $http.delete('/api/top5list/' + Id,  {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
            }).success(function (data) {
                $.each($scope.top5lists, function (i) {
                    if ($scope.top5lists[i].top5ListId === Id) {
                        $scope.top5lists.splice(i, 1);
                        return false;
                    }
                });
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Saving top5list! " + data;
                $scope.loading = false;
            });
        };

        //Delete top5list
        $scope.deletetop5listitem = function () {
            $scope.loading = true;
            var ItemId = this.top5listitem.top5ListItemId;
            var ListId = this.top5listitem.top5ListId;
            $http.delete('/api/top5listitem/' + ItemId, {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
            }).success(function (data) {
                $.each($scope.top5lists, function (i) {
                    if ($scope.top5lists[i].top5ListId === ListId) {
                        $.each($scope.top5lists[i].top5listitems, function (j) {
                            if ($scope.top5lists[i].top5listitems[j].top5ListItemId === ItemId) {
                                $scope.top5lists[i].top5listitems.splice(j, 1);
                                return false;
                            }
                        });
                        return false;
                    }
                });
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while Saving top5list item! " + data;
                $scope.loading = false;
            });
        };

    }

    function searchController($scope, $http) {

        $scope.searchtags = [];

        //get all top5list information
        $http.get('/api/top5list/', {
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
        }).success(function (data) {
            $scope.top5lists = data;
            $scope.loading = false;
        })
        .error(function () {
            $scope.error = "An Error has occured while loading top5lists!";
            $scope.loading = false;
        });

        $scope.tagAdded = function (tag) {
            $scope.loading = true;
            var tagtext = tag.text;
            var length = $scope.searchtags.length;
            var searchtags1 = $scope.searchtags;
            if (length > 0) {
                var url = '/api/Top5List/Search/';
                for (var index = 0; index < length; index++) {
                    url += searchtags1[index].text + '/';
                }
                $http.get(url, {
                    headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
                }).success(function (data) {
                    $scope.top5lists = data;
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "An Error has occured while Saving tag! " + data;
                    $scope.loading = false;
                });
            }
        };

        $scope.tagRemoved = function (tag) {
            $scope.loading = true;
            var length = $scope.searchtags.length;
            if (length == 0) {
                var tagtext = tag.text;
                $http.get('/api/Top5List/', {
                    headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
                }).success(function (data) {
                    $scope.top5lists = data;
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "An Error has occured while Saving tag! " + data;
                    $scope.loading = false;
                });
            }
            else {
                var url = '/api/Top5List/Search/';
                for (var index = 0; index < $scope.searchtags.length; index++) {
                    url += $scope.searchtags[index].text + '/';
                }
                $http.get(url, {
                    headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
                }).success(function (data) {
                    $scope.top5lists = data;
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "An Error has occured while Saving tag! " + data;
                    $scope.loading = false;
                });
            }
        };

        //Get top5list items
        $scope.showItems = function () {
            this.top5list.itemsMode = true;
            $scope.loading = true;
            var Id = this.top5list.top5ListId;
            $scope.tags = this.top5list.tags;
            $http.get('/api/top5list/' + Id + '/items/', {
                headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') }
            }).success(
            function (data) {
                $.each($scope.top5lists, function (i) {
                    if ($scope.top5lists[i].top5ListId === Id) {
                        $scope.top5lists[i].top5listitems = data;
                    }
                });
                $scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while getting items! " + data;
                $scope.loading = false;
            });
        };
    }

    function UploadCtrl ($scope, $http, $timeout, $upload) {
        $scope.upload = [];
        $scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2" };
 
        $scope.onFileSelect = function ($files) {
            //$files: an array of files selected, each file has name, size, and type.
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $scope.upload[index] = $upload.upload({
                        url: "./api/files/upload", // webapi url
                        method: "POST",
                        data: { fileUploadObj: $scope.fileUploadObj },
                        file: $file
                    }).progress(function (evt) {
                        // get upload percentage
                        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        console.log(data);
                    }).error(function (data, status, headers, config) {
                        // file failed to upload
                        console.log(data);
                    });
                })(i);
            }
        }
 
        $scope.abortUpload = function (index) {
            $scope.upload[index].abort();
        }
    }
})();