import { FC, useRef, useState } from "react";
import { Button } from "./ui/button";
import { Input } from "./ui/input";
import { Label } from "./ui/label";

interface SearchProps {
  id: string;
  label: string;
  onSearch: (value: string) => void;
  isLoading?: boolean;
}

const Search: FC<SearchProps> = ({
  id,
  label,
  onSearch,
  isLoading = false,
}: SearchProps) => {
  const inputRef = useRef<HTMLInputElement>(null);
  const [showError, setShowError] = useState(false);

  const handleSearch = () => {
    if (!inputRef.current?.value) {
      setShowError(true);
    } else {
      onSearch(inputRef.current?.value);
    }
  };

  const handleClick = () => {
    handleSearch();
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      handleSearch();
    }
  };

  return (
    <>
      <div className="flex flex-col gap-2 w-96">
        <Label htmlFor={id}> {label} </Label>
        <div className="flex gap-2">
          <Input
            onKeyDown={handleKeyDown}
            ref={inputRef}
            id={id}
            disabled={isLoading}
          />
          <Button isLoading={isLoading} onClick={handleClick}>
            Search
          </Button>
        </div>
        {showError && <Label> This field cannot be empty</Label>}
      </div>
    </>
  );
};

export default Search;
