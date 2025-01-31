import { FC } from "react";
import {
  TableCaption,
  TableHeader,
  TableRow,
  TableHead,
  TableBody,
  TableCell,
  Table,
} from "./ui/table";
import { Skeleton } from "./ui/skeleton";

interface MainTableProps {
  headings: string[];
  tableData: { [key: string]: number | string | boolean }[] | undefined;
  caption?: string;
  isLoading?: boolean;
}

const MainTable: FC<MainTableProps> = ({
  headings,
  tableData,
  caption,
  isLoading = false,
}) => {
  return (
    <Table>
      {caption ? <TableCaption> {caption} </TableCaption> : undefined}
      <TableHeader>
        <TableRow>
          {headings.map((heading) => (
            <TableHead key={heading}> {heading}</TableHead>
          ))}
        </TableRow>
      </TableHeader>
      <TableBody>
        {isLoading && (
          <>
            {[...Array(3)].map((_, i) => (
              <TableRow key={i}>
                {headings.map((heading) => (
                  <TableCell key={heading + i}>
                    <Skeleton className="h-4 w-20" />
                  </TableCell>
                ))}
              </TableRow>
            ))}
          </>
        )}
        {tableData &&
          tableData.length > 0 &&
          tableData.map((row, index) => (
            <TableRow key={"row" + index}>
              {Object.keys(row).map((key, index) => (
                <TableCell key={key + index}>{row[key]}</TableCell>
              ))}
            </TableRow>
          ))}
      </TableBody>
    </Table>
  );
};

export default MainTable;
