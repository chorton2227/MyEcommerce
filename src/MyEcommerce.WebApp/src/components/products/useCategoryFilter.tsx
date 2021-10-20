import {
  Link,
  List,
  ListItem,
  ListItemText,
  Skeleton,
  Typography,
} from "@mui/material";
import React, { ReactElement, useEffect, useState } from "react";
import {
  CatalogsApi,
  CategoryReadDto,
  Configuration,
} from "../../generated/product-service/dist/index";

const useCategoryFilter = (): [string | undefined, ReactElement] => {
  const [isLoading, setIsLoading] = useState(true);
  const [categories, setCategories] = useState<CategoryReadDto[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<
    CategoryReadDto | undefined
  >();

  const catalogsApiService = new CatalogsApi(
    new Configuration({
      basePath: process.env.NEXT_PUBLIC_PRODUCT_SERVICE_URL,
    })
  );

  const getCategories = async () => {
    setIsLoading(true);

    const response = await catalogsApiService.getCategories(
      "CatalogId-3bfc1e05-6ce6-42ca-90f6-71556e1f1c7e"
    );

    setCategories(response.data);
    setIsLoading(false);
  };

  useEffect(() => {
    getCategories();
  }, []); // eslint-disable-line react-hooks/exhaustive-deps

  const categoryFilterComponent = (
    <React.Fragment>
      <Typography variant="h5" mb={2}>
        Browse
      </Typography>
      {isLoading ? (
        <React.Fragment>
          <Skeleton variant="text" />
          <Skeleton variant="text" />
          <Skeleton variant="text" />
          <Skeleton variant="text" />
          <Skeleton variant="text" />
        </React.Fragment>
      ) : (
        categories.map((category: CategoryReadDto) => (
          <List key={category.id} sx={{ p: 0 }}>
            <ListItem disablePadding>
              <ListItemText
                sx={{
                  m: 0,
                  py: 1,
                  borderBottom: "solid 1px #efefef",
                  ":hover": {
                    cursor: "pointer",
                  },
                }}
                onClick={(e) => {
                  setSelectedCategory(category);
                }}
              >
                <Link underline="none">
                  {category.name === selectedCategory?.name ? (
                    <strong>{category?.name}</strong>
                  ) : (
                    <span>{category?.name}</span>
                  )}
                </Link>
              </ListItemText>
            </ListItem>
          </List>
        ))
      )}
    </React.Fragment>
  );
  return [selectedCategory?.name!, categoryFilterComponent];
};

export default useCategoryFilter;
