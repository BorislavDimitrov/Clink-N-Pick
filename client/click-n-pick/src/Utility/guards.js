import { isAdmin } from "./auth";
import { GetAuthToken } from "./auth";

export function authGuard() {
  const token = GetAuthToken();

  if (token === null) {
    window.location.href = "/";
  }

  return null;
}

export function authAdminGuard() {
  if (isAdmin() === false) {
    window.location.href = "/";
  }

  return null;
}
