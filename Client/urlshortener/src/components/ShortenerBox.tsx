import React, { useState } from "react";
import validator from "validator";
import { UrlShortenApiService } from "../api/UrlShortenApiService";
import { HttpClient } from "../helpers/HttpClient";
import { HttpClientBuilder } from "../helpers/HttpClientBuilder";
import { AppConfig } from "../config/AppConfig";

const urlShortenApiService = new UrlShortenApiService(
    new HttpClient(
        new HttpClientBuilder({
            headers: { "Content-Type": "application/json" }
        })
    )
);

export function Shortenerbox() {
    const [longUrl, setLongUrl] = useState('');
    const [shortUrl, setShortUrl] = useState('');
    const [urlInvalid, setUrlInvalid] = useState(false);
    const [serverError, setServerError] = useState(false);

    function submitRequest() {
        setUrlInvalid(false);
        setServerError(false);
        if (validator.isURL(longUrl)) {
            urlShortenApiService.shortenUrl(longUrl)
                .then((result) => {
                    setShortUrl(`${AppConfig.getClientURL()}${result}`);
                })
                .catch(error => {
                    setServerError(true);
                }
                )
        }
        else {
            setUrlInvalid(true);
        }
    }

    function resetStates() {
        setServerError(false);
        setUrlInvalid(false);
        setShortUrl('');
        setLongUrl('');
    }

    function handleLongUrlChange(e: any) {
        setLongUrl(e.target.value);
    }

    return (
        <div className='p-5 bg-white rounded-xl shadow-lg flex flex-col space-y-3 font-sans w-1/2 text-purple-600'>
            <label>
                <span className='block  text-left pb-2'>Shorten a long url</span>
                <input onChange={handleLongUrlChange} value={longUrl} type='url' name='LongUrl' placeholder='Enter long url'
                    className='float-left font-thin border border-black border-width-8 px-1 py-1 w-full' />
                {urlInvalid ?
                    <p className='float-left mt-2 text-red-600 text-lg'>
                        Please provide a valid URL
                    </p>
                    : null
                }
            </label>
            {shortUrl ?
                <div className='flex-col'>
                    <label className='w-12/12'>
                        <span className='block text-white text-left pb-2'>Your Short Url!</span>
                        <input value={shortUrl} type='url' name='ShortUrl' readOnly
                            className='float-left font-thin text-black border border-purple-600 border-width-8 px-1 py-2 w-full' />
                    </label>
                    <div className='block'>
                        <button type='button'
                            className='text-xl font-medium bg-purple-600 text-white border border-purple-600 rounded hover:bg-purple-800 mt-4 px-4 py-2'
                            onClick={resetStates}>
                            Create another</button>
                    </div>
                </div>
                :
                <div className="block">
                    <button type='button'
                        className='text-xl font-medium bg-purple-600 text-white border border-purple-600 rounded hover:bg-purple-800 mt-4 px-4 py-2'
                        onClick={submitRequest}>
                        Generate short url</button>
                </div>
            }
            {serverError ?
                <span className='float-left mt-2 text-red-600 text-lg'>Something went wrong! Please try again. </span>
                : null
            }
        </div>
    );
}