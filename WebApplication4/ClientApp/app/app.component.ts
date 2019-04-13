import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Coin } from './coin';
import { Product } from './product';

@Component({
    selector: 'app',
    providers: [DataService],
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    //кошелек машины
    vmWallet: Coin[];
    //кошелек клиента
    customerWallet: Coin[];
    //список продуктов
    products: Product[];
    //внесенная сумма
    currentMoney: number;

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.init();  
    }

    //получаем данные с сервера
    init() {
        console.log("INIT")
        this.dataService.getData()
            .subscribe((data: object) => this.setData(data));      

        this.dataService.getProducts()
            .subscribe((data: Product[]) => this.products = data);
    }

    addCoin(c: Coin) {

        this.dataService.addCoin(c)
            .subscribe((data: object) => {
                this.setData(data);
                alert("Добавлена монета ценностью: " + c.value);
            }
            );  
    }

    setData(data: object) {
        this.vmWallet = data["vmWallet"];
        this.customerWallet = data["customerWallet"];
        this.currentMoney = data["currentMoney"];
    }

    returnMoney() {
        this.dataService.returnMoney(this.currentMoney)
            .subscribe((data: object) => this.setData(data));  
    } 

    buy(item: Product) {
        if (this.currentMoney < item.price) {
            alert("Недостаточно Средств!")
        }
        else {
            this.dataService.buy(this.currentMoney, item)
                .subscribe((data: object) => {
                    this.dataService.getProducts()
                        .subscribe((data: Product[]) => { this.products = data; alert("Спасибо за покупку!"); });
                    this.setData(data);                  
                },
                () => alert("Невозможно выдать сдачу!")); 
        }
    }

}