import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  useSidebar,
} from "@/components/ui/sidebar";
import { Home, Trophy, User } from "lucide-react";
import { ThemeToggle } from "./ThemeToggle";
import { Link } from "@tanstack/react-router";

// Menu items.
const items = [
  {
    title: "Home",
    url: "/",
    icon: Home,
  },
  {
    title: "Hiscore",
    url: "hiscore",
    icon: Trophy,
  },
  {
    title: "My groups",
    url: "mygroups",
    icon: User,
  },
];

export const AppSidebar = () => {
  const { toggleSidebar, isMobile } = useSidebar();

  return (
    <Sidebar>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupLabel className="text-xl mb-4">
            Welcome
          </SidebarGroupLabel>
          <SidebarGroupContent>
            <SidebarMenu>
              {items.map((item) => (
                <SidebarMenuItem key={item.title}>
                  <SidebarMenuButton
                    onClick={() => (isMobile ? toggleSidebar() : null)}
                    asChild
                  >
                    <Link to={item.url} className="text-lg">
                      <item.icon />
                      <span>{item.title}</span>
                    </Link>
                  </SidebarMenuButton>
                </SidebarMenuItem>
              ))}
              <ThemeToggle />
            </SidebarMenu>
            <SidebarFooter />
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
    </Sidebar>
  );
};
