import { UrlShortenApiService } from "../api/UrlShortenApiService";
import { HttpClient } from "./HttpClient";
import { HttpClientBuilder } from "./HttpClientBuilder";

const urlShortenApiService = new UrlShortenApiService(
    new HttpClient(
        new HttpClientBuilder({
            headers: { "Content-Type": "application/json"}
        })
    )
);

function Redirecter(path: string){
    urlShortenApiService.FindLongUrl(path)
    .then((result) => {
        if(result){
            return(window.location = result);
        }
        return false;
    }).catch(error => {
        return false;
    });
    
}

export default Redirecter;