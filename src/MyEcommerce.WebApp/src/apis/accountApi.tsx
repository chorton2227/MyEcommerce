import {
  AccountApi,
  ForgotPasswordRequestDto,
  LoginRequestDto,
  LoginResponseDto,
  MeResponseDto,
  RegisterRequestDto,
  RegisterResponseDto,
  ResetPasswordRequestDto,
} from "../generated/identity-service/dist";

const accountApi = new AccountApi(
  undefined,
  process.env.NEXT_PUBLIC_IDENTITY_SERVICE_URL
);

export const forgotPassword = (
  forgotPasswordRequestDto?: ForgotPasswordRequestDto
): Promise<void> =>
  accountApi
    .forgotPassword(forgotPasswordRequestDto, { withCredentials: true })
    .then((response) => response.data);

export const login = (
  loginRequestDto?: LoginRequestDto
): Promise<LoginResponseDto> =>
  accountApi
    .login(loginRequestDto, { withCredentials: true })
    .then((response) => response.data);

export const logout = (): Promise<void> =>
  accountApi
    .logout({ withCredentials: true })
    .then((response) => response.data);

export const me = (): Promise<MeResponseDto> =>
  accountApi.me({ withCredentials: true }).then((response) => response.data);

export const register = (
  registerRequestDto?: RegisterRequestDto
): Promise<RegisterResponseDto> =>
  accountApi
    .register(registerRequestDto, { withCredentials: true })
    .then((response) => response.data);

export const resetPassword = (
  resetPasswordRequestDto?: ResetPasswordRequestDto
) =>
  accountApi
    .resetPassword(resetPasswordRequestDto, { withCredentials: true })
    .then((response) => response.data);
