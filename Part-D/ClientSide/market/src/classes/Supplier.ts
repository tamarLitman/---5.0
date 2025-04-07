import { Stock } from "./Stock"

export interface Supplier {
    SupplierId :Number;
    SupllierName :string|"";
    CompanyName :string|"";
    PhoneNumber :string|"";
    Representative :string|"";
    Stocks:Stock[];
}