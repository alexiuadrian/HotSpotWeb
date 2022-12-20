class Command < ApplicationRecord

    # relationships
    has_many :flags, dependent: :destroy
end
