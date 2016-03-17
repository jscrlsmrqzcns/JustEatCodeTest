interface RestaurantSearchControllerScope extends ng.IScope {
    control: RestaurantSearchController;
    queried: boolean; // has the user searched for restaurants at all?
    searching: boolean;
    outCode: string;
    restaurants: T4TS.RestaurantViewJsonModel[];
    errored: boolean;
    errorMessage: string;
}

class RestaurantSearchController {
    private scope: RestaurantSearchControllerScope;
    private restaurantSearchService: RestaurantSearchService;

    constructor($scope: RestaurantSearchControllerScope,
        restaurantSearchService: RestaurantSearchService) {

        this.scope = $scope;
        this.scope.control = this;
        this.restaurantSearchService = restaurantSearchService;
        
    }

    public searchRestaurants(f: FormData) {
        this.scope.errored = false;
        this.scope.queried = true;
        this.scope.searching = true;
        this.restaurantSearchService.searchByOutCode(this.scope.outCode)
            .then((restaurants) => {
                this.scope.restaurants = restaurants;
            })
            .catch((err) => {
                this.scope.errored = true;
                this.scope.errorMessage = err;
            })
            .finally(() => {
                this.scope.searching = false;
            });
    }

}