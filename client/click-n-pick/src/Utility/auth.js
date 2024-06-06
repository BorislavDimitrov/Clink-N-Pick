import { jwtDecode } from "jwt-decode";

export function SetAuthToken(token) {
  console.log(token);
  if (!token) {
    console.log(token);
  }
  localStorage.setItem("token", token);
}

export function GetAuthToken() {
  const token = localStorage.getItem("token");
  return token;
}

export function RemoveAuthToken() {
  localStorage.removeItem("token");
}

export const isAdmin = () => {
  const token = localStorage.getItem("token");
  if (!token) {
    return false;
  }

  try {
    const decoded = jwtDecode(token);
    console.log(decoded);
    return (
      decoded[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ] === "Administrator"
    );
  } catch (error) {
    console.error("Invalid token:", error);
    return false;
  }
};