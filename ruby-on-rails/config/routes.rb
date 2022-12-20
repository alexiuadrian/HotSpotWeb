Rails.application.routes.draw do
  resources :flags
  resources :commands

  get "/commands/:id/run", to: "commands#run_command"
  post "/repositories/create", to: "repositories#create_repository"
  delete "/repositories/delete", to: "repositories#delete_repository"
  get "/repositories/get", to: "repositories#get_repository"

  post "/repositories/create_organization", to: "repositories#create_organization_repository"
  delete "/repositories/delete_organization", to: "repositories#delete_organization_repository"
  get "/repositories/get_organization", to: "repositories#get_organization_repository"
end
