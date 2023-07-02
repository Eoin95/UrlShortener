import { AxiosRequestConfig } from "axios";
import { AppConfig } from "../config/AppConfig";
import { HttpClient } from "./HttpClient";

export class HttpClientBuilder {
    readonly configuration: AxiosRequestConfig;

    constructor(configuration: AxiosRequestConfig) {
        this.configuration = configuration;
        configuration.baseURL = AppConfig.getServerURL();
    }

    build(){
        return new HttpClient(this);
    }
}