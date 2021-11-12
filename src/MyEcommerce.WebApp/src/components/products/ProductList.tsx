import React, { useEffect, useReducer, useRef } from "react";
import {
  ProductReadDto,
  TagGroupSummaryDto,
  PaginatedProductsDto,
} from "../../generated/product-service/dist/index";
import {
  Box,
  Breadcrumbs,
  Button,
  Container,
  Divider,
  Drawer,
  Grid,
  LinearProgress,
  Link,
  Typography,
} from "@mui/material";
import ProductItem from "./ProductItem";
import usePageSort from "./usePageSort";
import usePromotionsFilter from "./usePromotionsFilter";
import useCategoryFilter from "./useCategoryFilter";
import useTagsFilter from "./useTagsFilter";
import usePriceFilter from "./usePriceFilter";
import ProductPageResults from "./ProductPageResults";
import { FilterList, NavigateNext } from "@mui/icons-material";
import { getAllProducts } from "../../apis/productsApi";

interface State {
  isLoading: boolean;
  isLoadingMore: boolean;
  hasMore: boolean;
  resetProducts: boolean;
  pageIndex: number;
  pageLimit: number;
  totalProducts: number;
  tagGroupSummaries: TagGroupSummaryDto[];
  products: ProductReadDto[];
  isMobileSidebarDrawerOpen: boolean;
}

type Action =
  | {
      type: "init";
    }
  | {
      type: "reset";
    }
  | {
      type: "loadMore";
    }
  | {
      type: "complete";
      paginatedProducts: PaginatedProductsDto;
    }
  | {
      type: "openMobileSidebarDrawer";
    }
  | {
      type: "closeMobileSidebarDrawer";
    };

const reducer = (state: State, action: Action): State => {
  switch (action.type) {
    case "loadMore":
      return {
        ...state,
        isLoadingMore: true,
        pageIndex: state.pageIndex + 1,
      };
    case "reset":
      return {
        ...state,
        resetProducts: true,
        pageIndex: 0,
      };
    case "complete":
      return {
        ...state,
        isLoading: false,
        isLoadingMore: false,
        resetProducts: false,
        totalProducts: action.paginatedProducts.totalProducts!,
        hasMore: action.paginatedProducts.hasMore ?? false,
        tagGroupSummaries: action.paginatedProducts.tagGroupSummaries!,
        products: state.resetProducts
          ? [...(action.paginatedProducts.products ?? [])]
          : [...state.products, ...(action.paginatedProducts.products ?? [])],
      };
    case "openMobileSidebarDrawer":
      return {
        ...state,
        isMobileSidebarDrawerOpen: true,
      };
    case "closeMobileSidebarDrawer":
      return {
        ...state,
        isMobileSidebarDrawerOpen: false,
      };
    default:
      return { ...state };
  }
};

const initialState: State = {
  isLoading: true,
  isLoadingMore: false,
  hasMore: false,
  resetProducts: false,
  pageIndex: 0,
  pageLimit: 12,
  totalProducts: 0,
  tagGroupSummaries: [],
  products: [],
  isMobileSidebarDrawerOpen: false,
};

const ProductList: React.FC<{}> = () => {
  const didMountReset = useRef(false);
  const [state, dispatch] = useReducer(reducer, initialState);
  const [pageSort, pageSortComponent] = usePageSort();
  const [isNew, onSale, promotionsFilterComponent] = usePromotionsFilter();
  const [selectedCategoryName, categoryFilterComponent] = useCategoryFilter();
  const [selectedTags, tagsFilterComponent] = useTagsFilter({
    tagGroupSummaries: state.tagGroupSummaries,
  });
  const [minPrice, maxPrice, priceFilterComponent] = usePriceFilter();

  const getProducts = async () => {
    const pageIndex = state.resetProducts ? 0 : state.pageIndex;
    const response = await getAllProducts(
      pageIndex,
      state.pageLimit,
      pageSort,
      isNew,
      onSale,
      minPrice,
      maxPrice,
      undefined,
      selectedCategoryName,
      selectedTags.map((selectedTag) => {
        return selectedTag.id!;
      })
    );

    dispatch({
      type: "complete",
      paginatedProducts: response,
    });
  };

  // init, loadMore, resetProducts
  useEffect(() => {
    // Halt when not loading or resetting
    if (!state.isLoading && !state.isLoadingMore && !state.resetProducts) {
      return;
    }

    (async () => {
      await getProducts();
    })();
  }, [state.isLoadingMore, state.resetProducts]); // eslint-disable-line react-hooks/exhaustive-deps

  // Reset after view changes
  useEffect(() => {
    if (!didMountReset.current) {
      didMountReset.current = true;
      return;
    }

    dispatch({ type: "reset" });
  }, [
    pageSort,
    isNew,
    onSale,
    minPrice,
    maxPrice,
    selectedTags,
    selectedCategoryName,
  ]);

  const sidebarDrawer = (
    <React.Fragment>
      {categoryFilterComponent}
      <Typography variant="h5" mt={4} mb={2}>
        Filter by
      </Typography>
      {priceFilterComponent}
      {promotionsFilterComponent}
      {tagsFilterComponent}
    </React.Fragment>
  );

  return (
    <section className="product-list">
      <Container sx={{ py: 4 }}>
        {state.isLoading ? (
          <LinearProgress />
        ) : (
          <React.Fragment>
            <Grid
              container
              spacing={2}
              mb={2}
              direction="row"
              alignItems="center"
              justifyContent="center"
              sx={{
                textAlign: { xs: "center", md: "unset" },
              }}
            >
              <Grid item xs={12} md={5}>
                <Box
                  sx={{
                    display: "flex",
                    justifyContent: { xs: "center", md: "unset" },
                  }}
                >
                  <Breadcrumbs separator={<NavigateNext />}>
                    <Link
                      href="/"
                      underline="none"
                      color="inherit"
                      sx={{ ":hover": { color: "initial" } }}
                    >
                      <Typography variant="h5">Home</Typography>
                    </Link>
                    <Link underline="none" color="inherit">
                      <Typography variant="h5">Shop</Typography>
                    </Link>
                    {!selectedCategoryName ? null : (
                      <Typography variant="h5" color="text.primary">
                        {selectedCategoryName}
                      </Typography>
                    )}
                  </Breadcrumbs>
                </Box>
              </Grid>
              <Grid item xs={12} sx={{ display: { xs: "block", md: "none" } }}>
                <Button
                  variant="outlined"
                  aria-label="Open mobile sidebar drawer"
                  onClick={() => {
                    dispatch({ type: "openMobileSidebarDrawer" });
                  }}
                  sx={{ display: { md: "none" } }}
                  startIcon={<FilterList />}
                >
                  Filter
                </Button>
                <Drawer
                  container={
                    window !== undefined
                      ? () => window.document.body
                      : undefined
                  }
                  variant="temporary"
                  open={state.isMobileSidebarDrawerOpen}
                  onClose={() => {
                    dispatch({ type: "closeMobileSidebarDrawer" });
                  }}
                  sx={{ display: { md: "none" } }}
                >
                  <Box px={2} py={4} minWidth={250}>
                    {sidebarDrawer}
                  </Box>
                </Drawer>
              </Grid>
              <Grid item xs={12} md={7}>
                <Grid
                  container
                  spacing={2}
                  direction="row"
                  alignItems="center"
                  justifyContent="center"
                >
                  <Grid item xs={12} md={6}>
                    <ProductPageResults
                      isLoading={
                        state.isLoading ||
                        state.isLoadingMore ||
                        state.resetProducts
                      }
                      pageIndex={state.pageIndex}
                      pageLimit={state.pageLimit}
                      totalProducts={state.totalProducts}
                    />
                  </Grid>
                  <Grid item xs={12} md={6}>
                    {pageSortComponent}
                  </Grid>
                </Grid>
              </Grid>
            </Grid>
            <Grid container spacing={2}>
              <Grid item md={3} sx={{ display: { xs: "none", md: "block" } }}>
                {sidebarDrawer}
              </Grid>
              <Grid container item xs={12} md={9} spacing={2}>
                {!state.products || state.products.length === 0 ? (
                  <Grid item xs={12}>
                    <Typography variant="h6" sx={{ mb: 1 }}>
                      No products were found matching your selection.
                    </Typography>
                    <Divider />
                  </Grid>
                ) : (
                  <React.Fragment>
                    {state.products.map((product: ProductReadDto) => (
                      <ProductItem
                        key={product.id}
                        isLoading={state.isLoading || state.resetProducts}
                        product={product}
                      />
                    ))}
                    <Grid item xs={12} sx={{ mt: 2 }}>
                      {state.isLoading || state.isLoadingMore ? (
                        <LinearProgress />
                      ) : (
                        <Button
                          fullWidth
                          variant="contained"
                          disabled={!state.hasMore}
                          onClick={() => dispatch({ type: "loadMore" })}
                        >
                          Load More
                        </Button>
                      )}
                    </Grid>
                  </React.Fragment>
                )}
              </Grid>
            </Grid>
          </React.Fragment>
        )}
      </Container>
    </section>
  );
};

export default ProductList;
