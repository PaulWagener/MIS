studiegidsApp.controller('CompetentieCtrl', function ($scope) {

    $scope.competenties = [];
    $scope.moduleCompetenties = [];
    $scope.niveaus = [];

    $scope.init = function (moduleCompetenties, options) {

        $scope.competenties = options.Competenties;
        $scope.niveaus = options.Niveaus;
        $scope.moduleCompetenties = moduleCompetenties;

    }

    $scope.removeCompetentie = function(index){
        $scope.moduleCompetenties.splice(index, 1);
    }

    $scope.addCompetentie = function () {
        $scope.moduleCompetenties.push({ nivea: "Beginner" });
    }
});