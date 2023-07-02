import { HttpClient } from "../helpers/HttpClient";
import { ShortenUrlRequest } from "../models/ShortenUrlRequest";

export class UrlShortenApiService {
    private httpClient: HttpClient;

    constructor(httpClient: HttpClient){
        this.httpClient = httpClient;
    }

    public FindLongUrl(shortUrlId: string){
        let request = this.httpClient.get(shortUrlId);
        return request.then(res =>{
            return res.data;
        })
        .then((result: any) => {
            return result.url;
        })
        .catch(error =>{
            const result = error.response;
            return Promise.reject(result);
        })
    }

    public shortenUrl(longUrl: string){
        let requestBody:  ShortenUrlRequest = {longUrl};
        let request = this.httpClient.post("shorten", requestBody);
        return request.then(res =>{
            return res.data;
        })
        .then((result: any) => {
            return result;
        })
        .catch(error =>{
            const result = error.response;
            return Promise.reject(result);
        })
    }
}