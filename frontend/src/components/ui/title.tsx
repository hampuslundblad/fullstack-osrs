import { cn } from "@/lib/utils";
import { FC } from "react";

interface TitleProps extends React.HTMLAttributes<HTMLHeadingElement> {
  children: React.ReactNode;
  props?: React.HTMLAttributes<HTMLHeadingElement>;
}

const Title: FC<TitleProps> = ({ children, props, className }) => {
  return (
    <h1 {...props} className={cn("text-3xl font-bold", className)}>
      {children}
    </h1>
  );
};

export default Title;
