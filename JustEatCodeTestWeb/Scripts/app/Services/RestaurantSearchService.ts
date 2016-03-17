class RestaurantSearchService {
    private $http: ng.IHttpService;
    private $q: ng.IQService

    constructor($http: ng.IHttpService, $q: ng.IQService) {
        this.$http = $http;
        this.$q = $q;
    }

    public searchByOutCode(outCode: string): ng.IPromise<T4TS.RestaurantViewJsonModel[]> {
        var restaurantsPromise = this.$q.defer<T4TS.RestaurantViewJsonModel[]>();

        this.$http.get<T4TS.RestaurantViewJsonModel[]>(urlPrefix + "/api/RestaurantsApi/" + outCode)
            .success((response) => {
                restaurantsPromise.resolve(response);
            })
            .error((err) => {
                restaurantsPromise.reject(err.ExceptionMessage);
            });

        return restaurantsPromise.promise;
    }
}