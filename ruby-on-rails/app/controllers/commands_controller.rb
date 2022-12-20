class CommandsController < ApplicationController
  before_action :set_command, only: %i[ show update destroy ]

  # GET /commands
  def index
    @commands = Command.all

    render json: @commands
  end

  # GET /commands/1
  def show
    execute = run_command_by_id(@command.id)

    if execute
      render json: @command
    else
      render json: @command.errors, status: :unprocessable_entity
    end
  end

  # POST /commands
  def create
    @command = Command.new(command_params)
    flags = params[:flags]
    flags and flags.each do |flag|
      new_flag = Flag.new(name: flag["name"], value: flag["value"])
      new_flag.save
      @command.flags << new_flag
    end

    # execute = run_command(@command)
    execute = true

    if execute
      if @command.save
        render json: @command, status: :created, location: @command
      else
        render json: @command.errors, status: :unprocessable_entity
      end
    else
      render json: @command.errors, status: :unprocessable_entity
    end
  end

  # PATCH/PUT /commands/1
  def update
    if @command.update(command_params)
      render json: @command
    else
      render json: @command.errors, status: :unprocessable_entity
    end
  end

  # DELETE /commands/1
  def destroy
    @command.destroy
  end

  private
    # Use callbacks to share common setup or constraints between actions.
    def set_command
      @command = Command.find(params[:id])
    end

    # Only allow a list of trusted parameters through.
    def command_params
      params.require(:command).permit(:name, :is_available, :platform, :requires_admin)
    end

  
  def run_command_by_id(id)
    system(create_command(Command.find(id)))
  end

  def run_command(command)
    system(create_command(command))
  end

  def create_command(command)
    flags = ""
    command.flags.each do |flag|
      flags += flag.name + " " + flag.value + " "
    end

    command.name + " " + flags
  end
end
