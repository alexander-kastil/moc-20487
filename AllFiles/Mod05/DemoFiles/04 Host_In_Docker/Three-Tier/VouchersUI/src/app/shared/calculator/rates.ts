export interface RatesParam{
  name: string;
  amount: number;
}

export interface RatesResponse {
  base: string;
  date: string;
  rates: Map<string, number>;
}

