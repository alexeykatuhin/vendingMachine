import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Coin } from './coin';
import { Product } from './product';

@Injectable()
export class DataService {
    constructor(private http: HttpClient) {

    }

    getData() {
        return this.http.get("/api/getdata/" );
    }

    getProducts() {
        return this.http.get("/api/products");
    }

    addCoin(coin: Coin) {
        return this.http.post("/api/addcoin", coin)
    }

    returnMoney(sum: number) {
        return this.http.post("/api/return", null);
    }

    buy(sum: number, product: Product) {
        return this.http.post("/api/buy/" + sum, product);
    }
}