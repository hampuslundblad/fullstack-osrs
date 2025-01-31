import GithubLight from "../../assets/github-mark-white.svg?react";
import GithubDark from "../../assets/github-mark.svg?react";

export interface IconProps extends React.SVGProps<SVGSVGElement> {
  theme: "dark" | "light";
}

export const GithubIcon = ({ theme, className, ...props }: IconProps) => {
  const GithubIcon = theme === "dark" ? GithubDark : GithubLight;

  return <GithubIcon viewBox="0 0 98 96" className={className} {...props} />;
};
