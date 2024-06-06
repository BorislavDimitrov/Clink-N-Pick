export function isEmailValid(email) {
  const regex = /^[a-zA-Z0–9._-]+@[a-zA-Z0–9.-]+\.[a-zA-Z]{2,4}$/;
  return regex.test(email);
}

export function isUsernameValid(username) {
  return hasMinLength(username, 5) && hasMaxLength(username, 30);
}

export function isPhoneNumberValid(phoneNumber) {
  if (!phoneNumber) {
    return true;
  }

  const regex = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/;
  return regex.test(phoneNumber);
}

export function isPasswordValid(password) {
  const regex =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$/;
  return regex.test(password);
}

export function isEqualsToOtherValue(value, otherValue) {
  return value === otherValue;
}

export function hasMinLength(value, minLength) {
  if (value === null || value === undefined) {
    return false;
  }
  return value.length >= minLength;
}

export function hasMaxLength(value, maxLength) {
  if (value === null || value === undefined) {
    return true;
  }

  return value.length <= maxLength;
}

export function isDescriptionValid(description) {
  if (!hasMinLength(description, 1)) {
    return false;
  }

  if (!hasMaxLength(description, 2000)) {
    return false;
  }

  return true;
}

export function isTitleValid(title) {
  if (!hasMinLength(title, 5)) {
    return false;
  }

  if (!hasMaxLength(title, 30)) {
    return false;
  }

  return true;
}

export function isPriceValid(price, minValue, maxValue) {
  if (price < minValue || price > maxValue) {
    return false;
  }

  return true;
}

export function isImageValid(image) {
  const validTypes = ["image/jpeg", "image/png", "image/gif", "image/jpg"];
  if (!validTypes.includes(image.type)) {
    return false;
  }

  const maxSizeInBytes = 2 * 1024 * 1024; // 2MB

  if (image.size > maxSizeInBytes) {
    return false;
  }

  return true;
}

export function areImagesValid(images, maxImagesCount) {
  if (images && images.length > maxImagesCount) {
    return false;
  }

  if (images) {
    for (let i = 0; i < images.length; i++) {
      if (!isImageValid(images[i])) {
        return false;
      }
    }
  }

  return true;
}
