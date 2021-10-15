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
} from "typescript-axios-product-service";

const useCategoryFilter = (): [string | undefined, ReactElement] => {
  const [isLoading, setIsLoading] = useState(true);
  const [categories, setCategories] = useState<CategoryReadDto[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<
    CategoryReadDto | undefined
  >();

  const catalogsApiService = new CatalogsApi(
    new Configuration({
      basePath: "https://localhost:6001",
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
  }, []);

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
          <List sx={{ p: 0 }}>
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
