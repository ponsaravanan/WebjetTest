(function () {
    'use strict';

    angular.module('MovieApp').
        factory('MovieService', ['$resource', '$cacheFactory', function ($resource, $cacheFactory) {
            return {
                SearchMovies: $resource('/api/moviedb/SearchMovies/:title'),
                GetMovie: $resource('api/moviedb/getmoviebyid/:provider/:id')

            }
        }])
})();