import {
  ProductPageSortDto,
  ProductReadDto,
  ProductsApi,
} from "../generated/product-service/dist";

const productsApi = new ProductsApi(
  undefined,
  process.env.NEXT_PUBLIC_PRODUCT_SERVICE_URL
);

export const getProductById = (
  id: string,
  options?: any
): Promise<ProductReadDto> =>
  productsApi.getById(id, options).then((response) => response.data);

export const getAllProducts = (
  pageIndex?: number,
  pageLimit?: number,
  pageSort?: ProductPageSortDto,
  filterIsNew?: boolean,
  filterOnSale?: boolean,
  filterMinPrice?: number,
  filterMaxPrice?: number,
  filterCategoryId?: string,
  filterCategoryName?: string,
  filterTags?: Array<string>,
  options?: any
) =>
  productsApi
    .getAll(
      pageIndex,
      pageLimit,
      pageSort,
      filterIsNew,
      filterOnSale,
      filterMinPrice,
      filterMaxPrice,
      filterCategoryId,
      filterCategoryName,
      filterTags,
      options
    )
    .then((response) => response.data);
