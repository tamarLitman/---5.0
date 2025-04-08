export interface Stock {
    prodId: number;
    prodName?: string;
    price: number;    
    minAmount?:number;
    supplierId?:Number;
}