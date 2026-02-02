export interface Category {
  id: number;
  name: string;
}

export interface Article {
  id: number;
  name: string;
  category: Category; 
  price: number;
}