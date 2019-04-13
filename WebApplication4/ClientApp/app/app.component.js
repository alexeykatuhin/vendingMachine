var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { DataService } from './data.service';
var AppComponent = /** @class */ (function () {
    function AppComponent(dataService) {
        this.dataService = dataService;
    }
    AppComponent.prototype.ngOnInit = function () {
        this.init();
    };
    //получаем данные с сервера
    AppComponent.prototype.init = function () {
        var _this = this;
        console.log("INIT");
        this.dataService.getData()
            .subscribe(function (data) { return _this.setData(data); });
        this.dataService.getProducts()
            .subscribe(function (data) { return _this.products = data; });
    };
    AppComponent.prototype.addCoin = function (c) {
        var _this = this;
        this.dataService.addCoin(c)
            .subscribe(function (data) {
            _this.setData(data);
            alert("Добавлена монета ценностью: " + c.value);
        });
    };
    AppComponent.prototype.setData = function (data) {
        this.vmWallet = data["vmWallet"];
        this.customerWallet = data["customerWallet"];
        this.currentMoney = data["currentMoney"];
    };
    AppComponent.prototype.returnMoney = function () {
        var _this = this;
        this.dataService.returnMoney(this.currentMoney)
            .subscribe(function (data) { return _this.setData(data); });
    };
    AppComponent.prototype.buy = function (item) {
        var _this = this;
        if (this.currentMoney < item.price) {
            alert("Недостаточно Средств!");
        }
        else {
            this.dataService.buy(this.currentMoney, item)
                .subscribe(function (data) {
                _this.dataService.getProducts()
                    .subscribe(function (data) { _this.products = data; alert("Спасибо за покупку!"); });
                _this.setData(data);
            }, function () { return alert("Невозможно выдать сдачу!"); });
        }
    };
    AppComponent = __decorate([
        Component({
            selector: 'app',
            providers: [DataService],
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css']
        }),
        __metadata("design:paramtypes", [DataService])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=app.component.js.map