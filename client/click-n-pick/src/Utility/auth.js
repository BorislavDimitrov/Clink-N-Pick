import { jwtDecode } from "jwt-decode";

export function SetAuthToken(token) {
  if (!token) {
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

export function GetCurrentUserId() {
  var token = GetAuthToken();

  if (token) {
    const decodedToken = jwtDecode(token);
    const userId =
      decodedToken[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
      ];

    return userId;
  } else {
    return null;
  }
}

export const isAdmin = () => {
  const token = localStorage.getItem("token");
  if (!token) {
    return false;
  }

  try {
    const decoded = jwtDecode(token);
    return (
      decoded[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ] === "Administrator"
    );
  } catch (error) {
    return false;
  }
};
