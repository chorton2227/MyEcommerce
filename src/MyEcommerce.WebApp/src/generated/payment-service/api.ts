/* tslint:disable */
/* eslint-disable */
/**
 * MyEcommerce.Services.PaymentService.API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import { Configuration } from './configuration';
import globalAxios, { AxiosPromise, AxiosInstance } from 'axios';
// Some imports not used depending on template conditions
// @ts-ignore
import { DUMMY_BASE_URL, assertParamExists, setApiKeyToObject, setBasicAuthToObject, setBearerAuthToObject, setOAuthToObject, setSearchParams, serializeDataIfNeeded, toPathString, createRequestFunction } from './common';
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, RequestArgs, BaseAPI, RequiredError } from './base';

/**
 * 
 * @export
 * @interface StripePaymentRequest
 */
export interface StripePaymentRequest {
    /**
     * 
     * @type {number}
     * @memberof StripePaymentRequest
     */
    amount?: number;
}
/**
 * 
 * @export
 * @interface StripePaymentResponse
 */
export interface StripePaymentResponse {
    /**
     * 
     * @type {string}
     * @memberof StripePaymentResponse
     */
    clientSecret?: string | null;
}

/**
 * PaymentsApi - axios parameter creator
 * @export
 */
export const PaymentsApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @param {StripePaymentRequest} [stripePaymentRequest] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        createAsync: async (stripePaymentRequest?: StripePaymentRequest, options: any = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/Payments`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;


    
            localVarHeaderParameter['Content-Type'] = 'application/json';

            setSearchParams(localVarUrlObj, localVarQueryParameter, options.query);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            localVarRequestOptions.data = serializeDataIfNeeded(stripePaymentRequest, localVarRequestOptions, configuration)

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * PaymentsApi - functional programming interface
 * @export
 */
export const PaymentsApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = PaymentsApiAxiosParamCreator(configuration)
    return {
        /**
         * 
         * @param {StripePaymentRequest} [stripePaymentRequest] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async createAsync(stripePaymentRequest?: StripePaymentRequest, options?: any): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<StripePaymentResponse>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.createAsync(stripePaymentRequest, options);
            return createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration);
        },
    }
};

/**
 * PaymentsApi - factory interface
 * @export
 */
export const PaymentsApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = PaymentsApiFp(configuration)
    return {
        /**
         * 
         * @param {StripePaymentRequest} [stripePaymentRequest] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        createAsync(stripePaymentRequest?: StripePaymentRequest, options?: any): AxiosPromise<StripePaymentResponse> {
            return localVarFp.createAsync(stripePaymentRequest, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * PaymentsApi - object-oriented interface
 * @export
 * @class PaymentsApi
 * @extends {BaseAPI}
 */
export class PaymentsApi extends BaseAPI {
    /**
     * 
     * @param {StripePaymentRequest} [stripePaymentRequest] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof PaymentsApi
     */
    public createAsync(stripePaymentRequest?: StripePaymentRequest, options?: any) {
        return PaymentsApiFp(this.configuration).createAsync(stripePaymentRequest, options).then((request) => request(this.axios, this.basePath));
    }
}


