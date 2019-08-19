export interface Item {
  unitPrice: number;
  units: number;
  vatPercentage: number;
  productCode: string;
  deliveryDate: any;
  description: string;
  category: string;
  name: string;
}

export interface PaymentAddress {
  City: string;
  Country: string;
  StreetAddress: string;
  PostalCode: string;
  County: string;
}

export interface Customer {
  email: string;
  firstName: string;
  lastName: string;
  phone: string;
  vatId: string;
}

export interface CreatePaymentRequest {
  items: Item[];
  stamp: string;
  reference: string;
  amount: number;
  currency: 'USD' | 'EUR';
  language: string;
  customer: Customer;
  deliveryAddress: any;
  invoicingAddress: any;
}

export const fakeCustomer : Customer = {
  email: 'something@mail.mail',
  firstName: 'Testi', lastName: 'Henkilo', phone: '0401234123', vatId: ''
}

// export const fakeAddress : PaymentAddress {

// }
