require "test_helper"

class CommandsControllerTest < ActionDispatch::IntegrationTest
  setup do
    @command = commands(:one)
  end

  test "should get index" do
    get commands_url, as: :json
    assert_response :success
  end

  test "should create command" do
    assert_difference("Command.count") do
      post commands_url, params: { command: { is_available: @command.is_available, name: @command.name, platform: @command.platform, requires_admin: @command.requires_admin } }, as: :json
    end

    assert_response :created
  end

  test "should show command" do
    get command_url(@command), as: :json
    assert_response :success
  end

  test "should update command" do
    patch command_url(@command), params: { command: { is_available: @command.is_available, name: @command.name, platform: @command.platform, requires_admin: @command.requires_admin } }, as: :json
    assert_response :success
  end

  test "should destroy command" do
    assert_difference("Command.count", -1) do
      delete command_url(@command), as: :json
    end

    assert_response :no_content
  end
end
