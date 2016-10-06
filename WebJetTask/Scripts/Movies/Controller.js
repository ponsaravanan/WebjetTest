(function () {
    'use strict';

    angular
        .module('MovieApp')
            .controller('MovieController', ['$scope', 'MovieService', '$http', '$filter',
                function ($scope, MovieService, $http, $filter) {
                    $scope.Message = "";
                    $scope.SearchInput = "";


                    $scope.SearchNow = function () {
                        $scope.Message = '';
                        LoadingStart();
                        $scope.Movies = MovieService.SearchMovies.query({ title: $scope.SearchInput }, function (result) {
                            $scope.Movies = result;
                            console.log(result);
                            CheckResults(result);

                            LoadingStop();
                            return $scope.Movies;
                        });
                    };

                    $scope.SelectMovie = function (SelectedOne) {
                        $scope.Message = '';

                        $scope.BestMovies = [];
                        LoadingStart();

                        for (var i = 0, len = SelectedOne.Provider.length; i < len; i++) {
                            //console.log('movie', SelectedOne.Provider);

                            var SelectedProvider = SelectedOne.Provider[i];
                            MovieService.GetMovie.get({ provider: SelectedProvider.MovieProvider, id: SelectedProvider.ID }, function (result) {
                                if (result.Price == undefined) {
                                    $scope.Message += "Service failed to fetch the price.";
                                } else {
                                    $scope.BestMovies.push(result);
                                    $scope.BestMovies.sort(function (a, b) {

                                        return (a['Price'] > b['Price']) ? 1 : ((a['Price'] < b['Price']) ? -1 : 0);
                                    });
                                    $scope.BestMovie = $scope.BestMovies[0];//first movie is the cheapest
                                }
                                LoadingStop();
                                return result;
                            });
                        }

                    };

                    function CheckResults(Result) {
                        try {
                            var AllProviders = ['cinemaworld', 'filmworld'];
                            if (Result.length > 0) {
                                if (Result[0].Provider.length < 2) {


                                    for (var i = 0, len = Result[0].Provider.length; i < len; i++) {
                                        var FoundIndex = AllProviders.indexOf(Result[0].Provider[i].MovieProvider)
                                        if (FoundIndex > -1) {
                                            AllProviders.splice(FoundIndex, 1);
                                        }


                                    }
                                    for (var i = 0, len = AllProviders.length; i < len; i++) {
                                        { $scope.Message += AllProviders[i] + " failed to load"; }
                                    }
                                }


                                //console.log("AllProviders", AllProviders);
                            }
                            } catch (ex) {
                                console.log(ex);
                            }
                        }

                        /****************************   remove later    *****************************/

                        $scope.SearchNow();

                        /*****************************************************************************/



                    }]);
                })();
