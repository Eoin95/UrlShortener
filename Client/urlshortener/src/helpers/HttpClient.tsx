import axios, { AxiosInstance, AxiosPromise, AxiosRequestConfig } from "axios";
import { HttpClientBuilder } from "./HttpClientBuilder";

export class HttpClient{

    public get<T = any>(
        url: string,
        config?: AxiosRequestConfig
    ): AxiosPromise<T> {
        return this.axios.get(url, config);
    }

    public post<T = any>(
        url: string,
        requestBody: any,
        config?: AxiosRequestConfig
    ): AxiosPromise<T> {
        return this.axios.post(url, requestBody, config);
    }
    
    private axios: AxiosInstance;
    constructor(builder: HttpClientBuilder){
        this.axios = axios.create(builder.configuration);
        this.axios.interceptors.response.use(this.handleSuccess, this.handleError);
    }

    private handleSuccess<V>(response: V): V | Promise<V>{
        return response;
    }

    private handleError(error: any): any{
        return Promise.reject(error);
    }
}