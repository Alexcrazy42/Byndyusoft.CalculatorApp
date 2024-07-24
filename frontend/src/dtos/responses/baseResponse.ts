export interface IBaseResponse<T> {
    isError: boolean
    isExpectedError: boolean,
    DefaultMessage: string,
    message: string,
    data: T
}