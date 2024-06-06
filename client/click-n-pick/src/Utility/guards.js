import { isAdmin } from "./auth";

export function authenticationGuard(token) {
  if (token === null) {
    window.location.href = "/login";
  }

  return null;
}

export function authAdminGuard() {
  if (isAdmin() === false) {
    window.location.href = "/login";
  }

  return null;
}
