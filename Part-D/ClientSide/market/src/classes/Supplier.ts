import { Stock } from "./Stock"

export interface Supplier {
    supplierId :Number;
    supllierName :string|"";
    companyName :string|"";
    phoneNumber :string|"";
    representative :string|"";
    stocks:Stock[];
}