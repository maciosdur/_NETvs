export type Category = 'Owoce' | 'Warzywa' | 'Nabiał' | 'Inne';

export interface Article {
  id: number;
  name: string;
  category: Category;
  price: number;
}