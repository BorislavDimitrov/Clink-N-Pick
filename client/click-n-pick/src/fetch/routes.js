const identityRoutes = {
  register: "api/Identity/Register",
  confirmEmail: "api/Identity/ConfirmEmail",
  login: "api/Identity/Login",
  forgotPasswordInput: "api/Identity/ForgotPasswordSend",
  forgotPasswordChange: "api/Identity/ForgotPasswordChange",
  passwordChange: "api/Identity/ChangePassword",
};

const usersRoutes = {
  editProfile: "api/Users/EditProfile",
  viewProfile: "api/Users/ViewProfile",
};

const productsRoutes = {
  getProductById: "api/Products/GetProduct",
  create: "api/Products/Create",
  myProducts: "api/Products/MyProducts",
  delete: "api/Products/Delete",
  getEditDetails: "api/Products/Edit/GetEditDetails",
  userProducts: "api/Products/UserProducts",
  edit: "api/Products/Edit",
  promote: "api/Products/Promote",
  search: "api/Products/Search",
  details: "api/Products/Details",
};

const categoriesRoutes = {
  getAll: "api/Categories/GetAll",
  delete: "api/Administration/Categories/Delete",
  edit: "api/Administration/Categories/Edit",
  create: "api/Administration/Categories/Create",
  getById: "api/Categories/GetById",
};

const paymentsRoutes = {
  createPaymentIntent: "api/Payments/CreatePaymentIntent",
};

const deliveryRoutes = {
  createLabel: "api/Delivery/CreateLabel",
  requestShipment: "api/Delivery/RequestShipment",
  cancelShipment: "api/Delivery/CancelShipment",
  declineShipment: "api/Delivery/DeclineShipment",
  getShipmentsToReceive: "api/Delivery/ShipmentsToReceive",
  getShipmentsToSend: "api/Delivery/ShipmentsToSend",
  acceptShipment: "api/Delivery/AcceptShipment",
  getCities: "api/Delivery/GetCities",
  getQuarters: "api/Delivery/GetQuarters",
  getStreets: "api/Delivery/GetStreets",
};

const promotionsRoutes = {
  getAll: "api/PromotionPricing/GetAll",
};

export {
  identityRoutes,
  productsRoutes,
  usersRoutes,
  categoriesRoutes,
  promotionsRoutes,
  paymentsRoutes,
  deliveryRoutes,
};
