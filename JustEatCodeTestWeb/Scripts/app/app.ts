class AppFactory {
    static create(name: string): ng.IModule {
        var app = angular.module(name, []);

        return app;
    }
}

var app = AppFactory.create('app');

app.service('restaurantSearchService', RestaurantSearchService);
app.controller('restaurantSearchController', RestaurantSearchController);