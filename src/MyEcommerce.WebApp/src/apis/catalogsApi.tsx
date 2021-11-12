import {
  CatalogsApi,
  CategoryReadDto,
} from "../generated/product-service/dist";

const catalogsApi = new CatalogsApi(
  undefined,
  process.env.NEXT_PUBLIC_PRODUCT_SERVICE_URL
);

export const getAllCategories = (
  catalogId: string,
  options?: any
): Promise<CategoryReadDto[]> =>
  catalogsApi
    .getCategories(catalogId, options)
    .then((response) => response.data);
