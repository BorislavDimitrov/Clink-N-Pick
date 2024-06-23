export function SetUserImageUrl(imageUrl) {
  localStorage.setItem("profileImageUrl", imageUrl);
}

export function GetUserImageUrl() {
  const imageUrl = localStorage.getItem("profileImageUrl");
  return imageUrl;
}

export function RemoveUserImageUrl() {
  localStorage.removeItem("profileImageUrl");
}
