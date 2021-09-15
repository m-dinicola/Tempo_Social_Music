"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var tempo_db_api_service_1 = require("./tempo-db-api.service");
describe('TempoDBAPIService', function () {
    var service;
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({});
        service = testing_1.TestBed.inject(tempo_db_api_service_1.TempoDBAPIService);
    });
    it('should be created', function () {
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=tempo-db-api.service.spec.js.map