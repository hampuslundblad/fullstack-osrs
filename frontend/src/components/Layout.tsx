import { ChevronLeft } from "lucide-react";
import Title from "./ui/title";
import { Link } from "@tanstack/react-router";

type LayoutProps = {
  children: React.ReactNode;
  title?: string;
  showBackButton?: boolean;
};

const Layout = ({ children, title, showBackButton = false }: LayoutProps) => {
  return (
    <div className="mx-12 w-full pt-20 mb-8">
      {showBackButton ? <BackButton /> : <span />}
      <div className="">
        {title && <Title className=""> {title} </Title>}
        {children}
      </div>
    </div>
  );
};

const BackButton = () => {
  return (
    <Link to=".." className="flex gap-2 my-2">
      <ChevronLeft /> Back
    </Link>
  );
};

export default Layout;
