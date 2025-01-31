import { CircleAlert, CheckCircle, Info, AlertTriangle } from "lucide-react";

import {
  Alert as ShadCnAlert,
  AlertDescription,
  AlertTitle,
} from "@/components/ui/alert";
import { cn } from "@/lib/utils";

type AlertProps = React.HTMLAttributes<HTMLDivElement> & {
  status?: "success" | "error" | "info" | "warning";
  title?: string;
  description?: string;
};

const iconMap = {
  success: CheckCircle,
  error: CircleAlert,
  info: Info,
  warning: AlertTriangle,
};

const borderColorMap = {
  success: "border-green-500",
  error: "border-red-500",
  info: "border-blue-500",
  warning: "border-yellow-500",
};

function Alert({
  status = "info",
  title,
  description,
  className,
  ...props
}: AlertProps) {
  const Icon = iconMap[status];
  const borderColorClass = borderColorMap[status];

  return (
    <ShadCnAlert
      className={cn(`border-l-4 ${borderColorClass}`, className)}
      {...props}
    >
      <Icon className="h-4 w-4" />
      {title && <AlertTitle className="font-bold">{title}</AlertTitle>}
      {description && <AlertDescription>{description}</AlertDescription>}
    </ShadCnAlert>
  );
}

export default Alert;
