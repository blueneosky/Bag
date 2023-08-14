export interface PagedResult<T> {
    pageIndex: number;
    pageSize: number;
    nbTotalPage: number;
    nbTotalResults: number;
    results: T[];
}
