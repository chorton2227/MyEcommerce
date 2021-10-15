import {
  Box,
  Chip,
  Link,
  List,
  ListItem,
  ListItemText,
  Typography,
} from "@mui/material";
import React, { ReactElement, useState } from "react";
import {
  TagReadDto,
  TagGroupSummaryDto,
  TagSummaryDto,
} from "typescript-axios-product-service";

interface TagsFilterProps {
  tagGroupSummaries: TagGroupSummaryDto[];
}

const useTagsFilter = ({
  tagGroupSummaries,
}: TagsFilterProps): [TagReadDto[], ReactElement] => {
  const [selectedTags, setSelectedTags] = useState<TagReadDto[]>([]);
  const tagsFilterComponent = (
    <React.Fragment>
      {tagGroupSummaries.map((tagGroupSummary) => (
        <React.Fragment>
          <Typography variant="h6" mt={2} mb={1}>
            {tagGroupSummary.group}
          </Typography>

          {!selectedTags ? null : (
            <Box>
              {selectedTags.map((tag) => (
                <Chip
                  sx={{ mr: 1, mb: 1 }}
                  label={`${tag.name}`}
                  onDelete={() => {
                    setSelectedTags(
                      selectedTags.filter((selectedTag: TagReadDto) => {
                        return (
                          selectedTag.name !== tag.name ||
                          selectedTag.group !== tag.group
                        );
                      })
                    );
                  }}
                />
              ))}
            </Box>
          )}

          {!tagGroupSummary.tagSummaries ? null : (
            <List sx={{ p: 0 }}>
              {tagGroupSummary.tagSummaries
                .filter((tagSummary: TagSummaryDto) => {
                  return !selectedTags.some((selectedTag) => {
                    return (
                      selectedTag.group == tagSummary.tag!.group &&
                      selectedTag.name == tagSummary.tag!.name
                    );
                  });
                })
                .map((tagSummary: TagSummaryDto) => (
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
                        setSelectedTags([...selectedTags, tagSummary.tag!]);
                      }}
                    >
                      <Link underline="none">
                        {tagSummary.tag!.name} ({tagSummary.numProducts})
                      </Link>
                    </ListItemText>
                  </ListItem>
                ))}
            </List>
          )}
        </React.Fragment>
      ))}
    </React.Fragment>
  );
  return [selectedTags, tagsFilterComponent];
};

export default useTagsFilter;
