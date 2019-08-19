import { Component, OnInit } from '@angular/core';
import { Item } from './item';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {

  public itemList: Item[] = [];

  public paymentMethods: any[] = [];

  public total: number = 0;

  constructor(readonly httpClient: HttpClient) { }

  ngOnInit(): void { }

  // I want to pay
  public startPaying(): void {
    // this.httpClient.post('/api/payment/create', {}).subscribe( res =>{
    //   console.log('api/payment/create');
    // });
    this.httpClient.post('/api/payment/calculate-total', this.itemList).subscribe((res: any) => {
      console.log('api/payment/calculate-total', res);
      this.total = res.total;
      this.paymentMethods = res.paymentProviders;
    })
  }

  public addItem(): void {
    const newItem: Item = {
      name: 'Something' + Math.random(),
      units: 1,
      unitPrice: 20.0,
      vatPercentage: 24.0,
      productCode: 'abc' + Math.random(),
      description: '',
      category: '', deliveryDate: new Date()
    }
    this.itemList.push(newItem);
    this.calculateTotal();
  }

  public removeItem(index: number): void {
    this.itemList.splice(index, 1);
    this.calculateTotal();
  }

  // Not the best approach , I knew
  private calculateTotal(): void {
    if (this.itemList.length === 0) {
      this.total = 0;
    }
  }

  public onSubmit(ev: Event): void {
    console.log('ev', ev)
    ev.currentTarget.dispatchEvent(ev);
  }


}
