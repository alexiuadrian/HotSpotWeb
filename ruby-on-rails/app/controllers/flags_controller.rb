class FlagsController < ApplicationController
  before_action :set_flag, only: %i[ show update destroy ]

  # GET /flags
  def index
    @flags = Flag.all

    render json: @flags
  end

  # GET /flags/1
  def show
    render json: @flag
  end

  # POST /flags
  def create
    @flag = Flag.new(flag_params)

    if @flag.save
      render json: @flag, status: :created, location: @flag
    else
      render json: @flag.errors, status: :unprocessable_entity
    end
  end

  # PATCH/PUT /flags/1
  def update
    if @flag.update(flag_params)
      render json: @flag
    else
      render json: @flag.errors, status: :unprocessable_entity
    end
  end

  # DELETE /flags/1
  def destroy
    @flag.destroy
  end

  private
    # Use callbacks to share common setup or constraints between actions.
    def set_flag
      @flag = Flag.find(params[:id])
    end

    # Only allow a list of trusted parameters through.
    def flag_params
      params.require(:flag).permit(:name, :value)
    end
end
