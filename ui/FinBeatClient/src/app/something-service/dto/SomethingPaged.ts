import {Something} from './Something';

export interface SomethingPaged {
  somethings: Something[],
  totalCount: number,
}
